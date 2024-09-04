// Copyright (c) Angouri 2021.
// This file from HonkPerf.NET project is MIT-licensed.
// Read more: https://github.com/asc-community/HonkPerf.NET

using System;
using System.Collections.Generic;
using System.Linq;
using HonkPerf.NET.RefLinq;
using System.Runtime.CompilerServices;
using Cathei.LinqGen;
using NUnit.Framework;
using StructLinq;
using Unity.PerformanceTesting;

public class Benchmarks
{
    private readonly int[] arr = new[]
    {
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
        1, 2, 3, 10, 20, 30, 502, 2342, 23, 234, 23, 2235, 32, 324322, 333,
    };


    [MethodImpl(MethodImplOptions.NoInlining)]
    static int GetThing() => 15;

    [Test, Performance]
    public void AppendPrepend()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("LINQ", () =>
        {
            var seq = Array.Empty<int>()
                .Append(2)
                .Append(5)
                .Append(10)
                .Prepend(15)
                .Prepend(3);
            var res = 0;
            foreach (var r in seq)
                res += r;
        });
        m("RefLinq", () =>
        {
            var seq = Array.Empty<int>()
                .ToRefLinq()
                .Append(2)
                .Append(5)
                .Append(10)
                .Prepend(15)
                .Prepend(3);
            var res = 0;
            foreach (var r in seq)
                res += r;
        });
        m("LinqGen", () =>
        {
            var seq = Array.Empty<int>()
                .Gen()
                .Append(2)
                .Append(5)
                .Append(10)
                .Prepend(15)
                .Prepend(3);
            var res = 0;
            foreach (var r in seq)
                res += r;
        });
    }
    
    [Test, Performance]
    public void SelectWhere()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for", () =>
        {
            var res = 0.0;
            for (var i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                c += 5;
                if (c % 2 == 0)
                {
                    res += (c - 6.0 / 15);
                }
            }
        });
        m("LINQ", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("RefLinq", () =>
        {
            var res = 0.0;
            var seq = arr
                    .ToRefLinq()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var seq = arr
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
    }

    [Test, Performance]
    public void SelectWhereIReadOnlyList()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for-each", () =>
        {
            var res = 0.0;
            foreach (var c in (IReadOnlyList<int>)arr)
            {
                var c2 = c + 5;
                if (c2 % 2 == 0)
                {
                    res += (c2 - 6.0 / 15);
                }
            }
        });
        m("LINQ", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("RefLinq", () =>
        {
            var res = 0.0;
            var seq = ((IReadOnlyList<int>)arr)
                    .ToRefLinq()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var seq = ((IReadOnlyList<int>)arr)
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var seq = ((IReadOnlyList<int>)arr)
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
    }

    [Test, Performance]
    public void SelectWhereIEnumerable()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for-each", () =>
        {
            var res = 0.0;
            foreach (var c in (IEnumerable<int>)arr)
            {
                var c2 = c + 5;
                if (c2 % 2 == 0)
                {
                    res += (c2 - 6.0 / 15);
                }
            }
        });
        m("LINQ", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        // m("RefLinq", () =>
        // {
        //     var res = 0.0;
        //     var seq = ((IEnumerable<int>)arr)
        //             .ToRefLinq()
        //             .Select(c => c + 5)
        //             .Where(c => c % 2 == 0)
        //             .Select(c => c - 6.0 / 15)
        //         ;
        //     foreach (var a in seq)
        //         res += a;
        // });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var seq = ((IEnumerable<int>)arr)
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var seq = ((IEnumerable<int>)arr)
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            foreach (var a in seq)
                res += a;
        });
    }

    [Test, Performance]
    public void SelectWhereClosure()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for", () =>
        {
            var res = 0.0;
            var local = GetThing();
            for (var i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                c += 5;
                if (c % 2 == 0)
                {
                    res += (c - 6.0 / local);
                }
            }
        });
        m("LINQ", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / local)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("RefLinq", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                    .ToRefLinq()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select((c, local) => c - 6.0 / local, local)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / local)
                ;
            foreach (var a in seq)
                res += a;
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(new LinqGenFunc(local))
                ;
            foreach (var a in seq)
                res += a;
        });
    }

    record struct LinqGenFunc(int local) : IStructFunction<int, double>
    {
        public double Invoke(int arg) => arg - 6.0 / local;
    }

    [Test, Performance]
    public void SelectWhereSum()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for", () =>
        {
            var res = 0.0;
            for (var i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                c += 5;
                if (c % 2 == 0)
                {
                    res += (c - 6.0 / 15);
                }
            }
        });
        m("LINQ", () =>
        {
            var seq = arr
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            _ = seq.Sum();
        });
        m("RefLinq", () =>
        {
            var res = 0.0;
            var seq = arr
                    .ToRefLinq()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            _ = seq.Sum();
        });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var seq = arr
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            _ = seq.Sum();
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / 15)
                ;
            _ = seq.Sum();
        });
    }

    [Test, Performance]
    public void SelectWhereZipSum()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("for", () =>
        {
            var res = 0.0;
            var local = GetThing();
            for (var i = 0; i < arr.Length; i++)
            {
                var c = arr[i];
                c += 5;
                if (c % 2 == 0)
                {
                    res += (c - 6.0 / local);
                }
            }
        });
        m("LINQ", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                .Select(c => c + 5)
                .Where(c => c % 2 == 0)
                .Select(c => c - 6.0 / local)
                .Zip(arr.Where(c => c % 2 == 1), (c, c2) => (c, c2))
                .Where(p => local > 10)
                .Select(p => p.Item1 * p.Item2)
            ;
            _ = seq.Sum();
        });
        m("RefLinq", () =>
        {
            var local = GetThing();
            var seq = arr
                .ToRefLinq()
                .Select(c => c + 5)
                .Where(c => c % 2 == 0)
                .Select((c, local) => c - 6.0 / local, local)
                .Zip(arr.ToRefLinq().Where(c => c % 2 == 1))
                .Where((p, local) => local > 10, local)
                .Select(p => p.Item1 * p.Item2)
            ;
            _ = seq.Sum();
        });
        m("StructLinq", () =>
        {
            var res = 0.0;
            var local = GetThing();
            var seq = arr
                    .ToStructEnumerable()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0 / local)
                    .Zip(arr.ToStructEnumerable().Where(c => c % 2 == 1))
                    .Where(p => local > 10)
                    .Select(p => p.Item1 * p.Item2)
                ;
            _ = seq.Sum();
        });
    }
    
    [Test, Performance]
    public void SelectWhereAppendPrepend()
    {
        var m = Utility.CreateMeasure(reportGC: true);
        m("LINQ", () =>
        {
            var seq = arr
                .Select(c => c + 5)
                .Where(c => c % 2 == 0)
                .Select(c => c - 6.0)
                .Append(3)
                .Append(5)
                .Prepend(3)
                .Concat(arr.Select(c => c / 1d))
                ;
            _ = seq.Sum() + seq.Max();
        });
        m("RefLinq", () =>
        {
            var seq = arr
                    .ToRefLinq()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0)
                    .Append(3)
                    .Append(5)
                    .Prepend(3)
                    .Concat(arr.ToRefLinq().Select(c => c / 1d))
                ;
            _ = seq.Sum() + seq.Max();
        });
        m("LinqGen", () =>
        {
            var res = 0.0;
            var seq = arr
                    .Gen()
                    .Select(c => c + 5)
                    .Where(c => c % 2 == 0)
                    .Select(c => c - 6.0)
                    .Append(3)
                    .Append(5)
                    .Prepend(3)
                    .Concat(arr.Gen().Select(c => c / 1d))
                ;
            foreach (var a in seq)
                res += a;
        });
    }
}