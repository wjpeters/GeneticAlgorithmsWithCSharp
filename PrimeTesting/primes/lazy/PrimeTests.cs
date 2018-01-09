﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PrimeTesting.primes.lazy
{
    [TestClass]
    public class PrimeTests
    {
        public static LazyList<int> From(int n)
        {
            return new LazyList<int>(n, new Lazy<IMyList<int>>(() => From(n + 1)));
        }

        public static IMyList<int> Primes(IMyList<int> numbers)
        {
            return new LazyList<int>(
                numbers.Head, new Lazy<IMyList<int>>(() =>
                    Primes(
                        numbers.Tail.Filter(n => n % numbers.Head != 0)
                    )));
        }

        [TestMethod]
        public void LazyListTest()
        {
            var numbers = From(2);
            Assert.AreEqual(2, numbers.Head);
            Assert.AreEqual(3, numbers.Tail.Head);
            Assert.AreEqual(4, numbers.Tail.Tail.Head);
        }

        [TestMethod]
        public void PrimeTest()
        {
            var numbers = Primes(From(2));
            Assert.AreEqual(2, numbers.Head);
            Assert.AreEqual(3, numbers.Tail.Head);
            Assert.AreEqual(5, numbers.Tail.Tail.Head);
        }

        [TestMethod]
        public void PrintPrimes()
        {
            var numbers = Primes(From(2));
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine(numbers.Head);
                numbers = numbers.Tail;
            }
        }
    }
}