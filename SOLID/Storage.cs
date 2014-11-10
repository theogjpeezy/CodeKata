using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace SOLID {
	// Single responsibility principle
	// - Accessing a store and reporting metrics on accessing that store

	// Open-closed principle
	// - Pose the "It works fine over here, we want a remote/local store version over there."
	// finer point - subclassing and parameterization

	// Liskov substitution principle
	// - Remote store can sometimes throw timeout exceptions, problem?

	// Interface segregation principle
	// - Add a method without requiring old implementations to account for it

	// Dependency inversion principle
	// - checking a configuration value and instantiating a different service
	// finer point - relation to inversion of control containers

	public class Storage
	{
		private readonly Dictionary<string, object> _dictionary;
		private readonly Hashtable _hashtable;
		private readonly Dictionary<string, List<Tuple<DateTime,TimeSpan>>> _getRequests = new Dictionary<string, List<Tuple<DateTime, TimeSpan>>>();
		private Dictionary<string, List<Tuple<DateTime,TimeSpan>>> _setRequests = new Dictionary<string, List<Tuple<DateTime, TimeSpan>>>();
		private readonly Medium _storageChoice;

		public enum Medium
		{
			HashSet,
			Dictionary
		}

		public Storage(Medium choice)
		{
			_storageChoice = choice;
			switch (choice)
			{
				case Medium.HashSet:
					_hashtable = new Hashtable();
					break;
				case Medium.Dictionary:
					_dictionary = new Dictionary<string, object>();
					break;
				default:
					throw new ArgumentOutOfRangeException("choice");
			}
		}


		public object Get(string key)
		{
			DateTime requestStart = DateTime.Now;
			Stopwatch timer = Stopwatch.StartNew();
			try 
			{
				switch (_storageChoice)
				{
					case Medium.HashSet:
						return _hashtable[key];
					case Medium.Dictionary:
						return _dictionary[key];
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			finally
			{
				timer.Stop();
				if (_getRequests.ContainsKey(key))
				{
					_getRequests[key].Add(Tuple.Create(requestStart, timer.Elapsed));
				}
				else
				{
					_getRequests.Add(key, new List<Tuple<DateTime, TimeSpan>> { Tuple.Create(requestStart, timer.Elapsed)});
				}
			}
		}

		public void Set(string key, object value)
		{
			DateTime requestStart = DateTime.Now;
			Stopwatch timer = Stopwatch.StartNew();
			try
			{
				switch (_storageChoice)
				{
					case Medium.HashSet:
						_hashtable[key] = value;
						return;
					case Medium.Dictionary:
						_dictionary[key] = value;
						return;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			finally
			{
				timer.Stop();
				if (_setRequests.ContainsKey(key))
				{
					_setRequests[key].Add(Tuple.Create(requestStart, timer.Elapsed));
				}
				else
				{
					_setRequests.Add(key, new List<Tuple<DateTime, TimeSpan>>{ Tuple.Create(requestStart, timer.Elapsed) });
				}
			}
		}

		public ReadOnlyDictionary<string, List<Tuple<DateTime, TimeSpan>>>  GetMetrics
		{
			get
			{
				return new ReadOnlyDictionary<string, List<Tuple<DateTime, TimeSpan>>>(_getRequests);
			}
		}

		public ReadOnlyDictionary<string, List<Tuple<DateTime, TimeSpan>>> SetMetrics
		{
			get
			{
				return new ReadOnlyDictionary<string, List<Tuple<DateTime, TimeSpan>>>(_setRequests);
			}
		}
	}
}
