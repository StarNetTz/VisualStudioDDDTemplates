using NUnit.Framework;
using $projectname$.PL.Commands;
using $projectname$.PL.Events;
using System;
using System.Threading.Tasks;

namespace $rootnamespace$.$fileinputname$Tests
{
    class Create$fileinputname$Tests : _ServiceSpec
    {
        Create$fileinputname$ Create$fileinputname$Command;
        $fileinputname$Created $fileinputname$CreatedEvent;

        protected string AggregateId = "$fileinputname$s-1";

        [SetUp]
        public void Setup()
        {
            Create$fileinputname$Command = new Create$fileinputname$() { Id = AggregateId, IssuedBy = "zeko", Name = "Aggregate name", TimeIssued = DateTime.MinValue};
            $fileinputname$CreatedEvent = new $fileinputname$Created() { Id = AggregateId, IssuedBy = "zeko", Name = "Aggregate name", TimeIssued = DateTime.MinValue};
        }

        [Test]
        public async Task can_create_$fileinputname$()
        {
            Given();
            When(Create$fileinputname$Command);
            await Expect($fileinputname$CreatedEvent);
        }

        [Test]
        public async Task is_idempotent()
        {
            Given($fileinputname$CreatedEvent);
            When(Create$fileinputname$Command);
            await Expect();
        }

        [Test]
        public async Task cannot_create_$fileinputname$_that_is_already_created()
        {
            Given($fileinputname$CreatedEvent);
            var when = $fileinputname$CreatedEvent;
            when.Name = "new Name";
            When(when);
            await ExpectError("$fileinputname$AlreadyExists");
        }
    }
}