using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Discode.EventSystem
{
	/// <summary>
	/// A pool of objects for reuse
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class ObjectPool<T> where T : new()
	{
		/// <summary>
		/// Number to grow when needed
		/// </summary>
		private int growth = 20;

		/// <summary>
		/// Object ppol
		/// </summary>
		private T[] pool;

		/// <summary>
		/// Index counter
		/// </summary>
		private int nextObject = 0;

		public ObjectPool(int size)
		{
			Resize(size, false);
		}

		public ObjectPool(int size, int growSize)
		{
			growth = growSize;
			Resize(size, false);
		}

		public int Length
		{
			get { return pool.Length; }
		}

		public int AllocatedCount
		{
			get { return nextObject; }
		}

		public T AllocateObject()
		{
			T item = default(T);

			if (nextObject >= pool.Length)
			{
				if (growth > 0)
				{
					Resize(pool.Length + growth, true);
				}
				else
				{
					return item;
				}
			}

			if (nextObject >= 0 && nextObject < pool.Length)
			{
				item = pool[nextObject];
				nextObject++;
			}

			return item;
		}

		/// <summary>
		/// Sends an object back into the pool
		/// </summary>
		/// <param name="instance"></param>
		public void Release(T instance)
		{
			if (nextObject > 0)
			{
				nextObject--;
				pool[nextObject] = instance;
			}
		}

		public void Reset()
		{
			int length = growth;
			if (pool != null)
			{
				length = pool.Length;
			}

			Resize(length, false);

			nextObject = 0;
		}

		public void Resize(int size, bool preserve)
		{
			lock(this)
			{
				int count = 0;

				T[] newPool = new T[size];

				if (pool != null && preserve)
				{
					count = pool.Length;
					Array.Copy(pool, newPool, Math.Min(count, size));
				}

				for (int i = count; i < size; i++)
				{
					newPool[i] = new T();
				}

				pool = newPool;
			}
		}
	}
}
