﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace Vostok.Commons.Time.Tests
{
    [TestFixture]
    internal class TimeSpanArithmetics_Tests
    {
        [Test]
        public void Multiply_should_return_correct_value()
        {
            2.Seconds().Multiply(1.5).Should().Be(3.Seconds());
        }

        [Test]
        public void Divide_should_return_correct_value()
        {
            3.Seconds().Divide(1.5).Should().Be(2.Seconds());
        }

        [Test]
        public void Abs_should_return_input_value_for_positive_spans()
        {
            1.Seconds().Abs().Should().Be(1.Seconds());
        }

        [Test]
        public void Abs_should_return_input_value_for_zero_spans()
        {
            0.Seconds().Abs().Should().Be(0.Seconds());
        }

        [Test]
        public void Abs_should_return_negated_value_for_negative_spans()
        {
            (-1).Seconds().Abs().Should().Be(1.Seconds());
        }

        [Test]
        public void Min_should_return_minimum_value_of_given_two()
        {
            TimeSpanArithmetics.Min(1.Seconds(), 2.Seconds()).Should().Be(1.Seconds());
            TimeSpanArithmetics.Min(2.Seconds(), 1.Seconds()).Should().Be(1.Seconds());
        }

        [Test]
        public void Min_should_return_minimum_value_of_given_three()
        {
            TimeSpanArithmetics.Min(1.Seconds(), 2.Seconds(), 3.Seconds()).Should().Be(1.Seconds());
            TimeSpanArithmetics.Min(3.Seconds(), 1.Seconds(), 2.Seconds()).Should().Be(1.Seconds());
            TimeSpanArithmetics.Min(2.Seconds(), 3.Seconds(), 1.Seconds()).Should().Be(1.Seconds());
        }

        [Test]
        public void Max_should_return_minimum_value_of_given_two()
        {
            TimeSpanArithmetics.Max(1.Seconds(), 2.Seconds()).Should().Be(2.Seconds());
            TimeSpanArithmetics.Max(2.Seconds(), 1.Seconds()).Should().Be(2.Seconds());
        }

        [Test]
        public void Max_should_return_minimum_value_of_given_three()
        {
            TimeSpanArithmetics.Max(1.Seconds(), 2.Seconds(), 3.Seconds()).Should().Be(3.Seconds());
            TimeSpanArithmetics.Max(3.Seconds(), 1.Seconds(), 2.Seconds()).Should().Be(3.Seconds());
            TimeSpanArithmetics.Max(2.Seconds(), 3.Seconds(), 1.Seconds()).Should().Be(3.Seconds());
        }

        [Test]
        public void Clamp_should_return_value_when_in_segment()
        {
            1.Seconds().Clamp(0.Seconds(), 2.Seconds()).Should().Be(1.Seconds());
        }

        [Test]
        public void Clamp_should_return_min_when_time_less_than_min()
        {
            1.Seconds().Clamp(2.Seconds(), 3.Seconds()).Should().Be(2.Seconds());
        }

        [Test]
        public void Clamp_should_return_max_when_time_greater_than_max()
        {
            3.Seconds().Clamp(0.Seconds(), 1.Seconds()).Should().Be(1.Seconds());
        }

        [Test]
        public void Clamp_should_throw_argument_exception_when_max_less_than_min()
        {
            Assert.Throws<ArgumentException>(() => 1.Seconds().Clamp(3.Seconds(), 1.Seconds()));
        }
    }
}