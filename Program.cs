using System;
using System.Collections.Generic;

var irwin = new Actor("Steve", "Irwin");
var buscemi = irwin with { LastName = "Buscemi", Role = "Donny" };
var jeff = new Actor("Jeff", "Bridges", "The Dude", 5000000);
var john = new Actor("John", "Goodman", "Walter", 1500);
var ethan = new Director("Ethan", "Coen", "The Big Lebowski");
var joel = new Director("Joel", "Coen");
var reviewer = new Reviewer("Tom", "IMDB");

List<Person> cast = new();
cast.Add(irwin);
cast.Add(buscemi);
cast.Add(jeff);
cast.Add(john);
cast.Add(ethan);
cast.Add(joel);

var (dirFirst, dirLast, _) = ethan;
Console.WriteLine(dirFirst, dirLast);
var (jeffFirst, _, jeffRole, jeffPay) = jeff;
Console.WriteLine(jeffRole);
var (revName, _) = reviewer;
Console.WriteLine(revName);

var movie = new Movie("The big Lebowski", new() { ReleaseDate = new DateTime(1998, 3, 6)});

static string getCredit(Person p) => p switch {
    Actor a => a.Role is not null ? $"{a.Role} - {a.FirstName} {a.LastName}" : null,
    Director => $"Directed By {p.FirstName} {p.LastName}",
    _ => null
};

Console.WriteLine("\n");
Console.WriteLine(movie.name);
Console.WriteLine($"ReleaseDate: {movie.options.ReleaseDate.ToShortDateString()}");

if(movie.options.Category.IsLetter()) {
    Console.WriteLine($"Category: {movie.options.Category}");
}
Console.WriteLine("Credits: ");
foreach(var person in cast) {
    var credit = getCredit(person);
    if(credit is not null) {
        Console.WriteLine(getCredit(person));
    }
}

public static class DoubleExtensions {
    public static bool IsLetter(this char c) =>
        c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');
}

public record MovieOptions(double? Budget = 70000000, DateTime ReleaseDate = new DateTime(), char Category = 'A');
public record Movie(string name, MovieOptions options);

public record Person {
    public string FirstName { get;}
    public string LastName { get; init;}

    public Person(string firstName, string lastName) {
        FirstName = firstName;
        LastName = lastName;
    }
}

public record Actor: Person {
    public string Role { get; init; }
    public double? Pay { get; init; }

    public Actor(string firstName, string lastName, string role = null, double? pay = null): base(firstName, lastName) => (Role, Pay) = (role, pay);

    public void Deconstruct(out string first, out string last, out string role, out double? pay) {
        first = FirstName;
        last = LastName;
        role = Role;
        pay = Pay;
    }
    
}

public record Director(string FirstName, string LastName, string Movie = null): Person(FirstName, LastName);

public class Reviewer {
    public string Name {get;}
    public string Organization {get;}

    public Reviewer(string name, string org) => (Name, Organization) = (name, org);

    public void Deconstruct(out string name, out string org) {
        name = Name;
        org = Organization;
    }
}