﻿namespace csteeves.Advent2023;

public class RangeMapper {

    private List<long> unsortedRedirects = [];
    private Dictionary<long, Range> redirectLookup = [];
    private bool dirty = false;

    private List<long> Redirects {
        get {
            if (dirty) {
                unsortedRedirects.Sort();
                dirty = false;
            }
            return unsortedRedirects;
        }
    }

    public void AddMapping(string line) {
        List<long> tokens = LineParser.Tokens(line).Select(long.Parse).ToList();
        AddMapping(tokens[1], tokens[0], tokens[2]);
    }

    public void AddMapping(long redirect, long newValue, long range) {
        unsortedRedirects.Add(redirect);
        redirectLookup[redirect] = new Range(newValue, range);
        dirty = true;
    }

    public long GetRedirect(long input) {
        if (input < Redirects.First()) {
            return input;
        }

        long redirect = Redirects.Last(j => j <= input);
        Range range = redirectLookup[redirect];

        if (input <= redirect + range.length) {
            return range.start + input - redirect;
        }

        return input;
    }

    public IEnumerable<Range> GetRedirects(long start, long length) {
        return GetRedirects(0, start, length);
    }

    private IEnumerable<Range> GetRedirects(int index, long start, long length) {
        if (index > Redirects.Count || length < 0 || start < 0) {
            throw new ArgumentOutOfRangeException();
        }

        if (index == Redirects.Count) {
            yield return new Range(start, length);
            yield break;
        }

        long redirect = Redirects[index];
        Range range = redirectLookup[redirect];
        while (start > redirect + range.length - 1) {
            index++;
            if (Redirects.Count == index) {
                yield return new Range(start, length);
                yield break;
            }
            redirect = Redirects[index];
            range = redirectLookup[redirect];
        }

        if (start < redirect) {
            long preRange = Math.Min(length, redirect - start);
            yield return new Range(start, preRange);

            start += preRange;
            length -= preRange;
        }

        if (length == 0) {
            yield break;
        }

        long rangeStart = range.start + start - redirect;
        long remainingRange = range.length - start + redirect;
        long containedRange = Math.Min(length, remainingRange);
        yield return new Range(rangeStart, containedRange);

        start += containedRange;
        length -= containedRange;

        if (length == 0) {
            yield break;
        }

        foreach (Range subsequentRange in GetRedirects(index + 1, start, length)) {
            yield return subsequentRange;
        }
    }

    public void PrettyPrint() {
        foreach (long redirect in Redirects) {
            Range range = redirectLookup[redirect];
            Console.WriteLine(
                $"{redirect}-{redirect + range.length}"
                + $" => {range.start}-{range.start + range.length}");
        }
    }

}