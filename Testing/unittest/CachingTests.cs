﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VDS.RDF.Configuration;
using VDS.RDF.Parsing;

namespace VDS.RDF.Test
{
    [TestClass]
    public class CachingTests
    {
        private static Uri test = new Uri("http://api.talis.com/stores/rvesse-dev1/meta?about=" + Uri.EscapeDataString("http://example.org/vehicles/FordFiesta"));

        [TestMethod]
        public void UriLoaderCache()
        {
            //Load the Graph
            Graph g = new Graph();
            UriLoader.Load(g, test);

            //Then reload the Graph which it should now come from the cache instead
            Graph h = new Graph();
            UriLoader.Load(h, test);

            Assert.AreEqual(g, h);
        }

        [TestMethod]
        public void UriLoaderCustomCache()
        {
            UriLoader.CacheDirectory = "E:\\Cache";

            this.UriLoaderCache();
        }

        [TestMethod]
        public void UriLoaderUriSantisation()
        {
            Uri a = new Uri(ConfigurationLoader.ConfigurationNamespace + "TripleStore");
            Uri b = new Uri(ConfigurationLoader.ConfigurationNamespace + "Graph");

            Console.WriteLine("URI A: " + a.ToString() + " is equivalent to " + Tools.StripUriFragment(a).ToString());
            Console.WriteLine("URI B:" + b.ToString() + " is equivalent to " + Tools.StripUriFragment(b).ToString());

            Assert.AreEqual(Tools.StripUriFragment(a).ToString(), Tools.StripUriFragment(b).ToString(), "URIs stripped of their Fragment IDs should have been equal");

            Graph g = new Graph();
            UriLoader.Load(g, a);

            Assert.IsTrue(UriLoader.IsCached(a), "Content should have been cached as a result of loading from the URI");

            Graph h = new Graph();
            UriLoader.Load(h, b);

            Assert.AreEqual(g, h, "Two Graphs should be equal since they come from the same URI");
        }
    }
}
