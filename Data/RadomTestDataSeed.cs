using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Data {
    /// Contains Databse Seed definitions for the default data used by Campus Consult e.V.
    public class RandomTestDataSeed {

        public static readonly string[] WORDS = new string[]{"Lorem", "Ipsum", "Dolar", "Si", "Achmet", "Greetings", "From", "Germany", "Beer", "Campus", "Consult"};
        public static readonly Gender[] ALL_GENDER = new Gender[]{Gender.FEMALE, Gender.MALE, Gender.DIVERSE};
        public static async Task DoSeed(ApplicationDbContext connection, RandomTestSeedConfig config) {
            Console.WriteLine(config.Enabled + " " + config.Seed);
            if (!config.Enabled) {
                return;
            }
            Random rand;
            if (config.Seed.HasValue) {
                rand = new Random(config.Seed ?? 0);
            } else {
                rand = new Random();
            }
            DateTime start = new DateTime(1990, 1, 1);
            DateTime end = new DateTime(2012,12,21);
            for (int i = 0;i < config.Persons;i++) {
                await connection.Persons.AddAsync(new Person() {
                    AdressCity = RandomWord(rand),
                    AdressNr = rand.Next(300).ToString(),
                    AdressStreet = RandomSentence(rand, 1, 3),
                    AdressZIP = rand.Next(100000).ToString(),
                    Birthdate = RandomDate(rand, start, end),
                    EmailAssociaton = RandomChars(rand, 5, 9) + "@campus-consult.org",
                    EmailPrivate = RandomChars(rand, 5, 9) + "@gmail.com",
                    FirstName = RandomWord(rand),
                    Gender = RandomGender(rand),
                    LastName = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 5, 13),
                    MobilePrivate = rand.Next(1000000).ToString(),
                });
            }
            for (int i = 0;i < config.CareerLevels;i++) {
                string levelName = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 5, 10);
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = rand.Next(2) == 0,
                    Name = levelName,
                    ShortName = levelName.Substring(0,1),
                });
            }
            for (int i = 0;i < config.Positions;i++) {
                string levelName = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 5, 10);
                await connection.Positions.AddAsync(new Position() {
                    IsActive = rand.Next(2) == 0,
                    Name = levelName,
                    ShortName = levelName.Substring(0,1),
                });
            }
            await connection.SaveChangesAsync();
        }

        public static string RandomSentence(Random random, int minlength, int maxlength) {
            int length = random.Next(minlength, maxlength);
            string result = "";
            for (int i = 0;i < length;i++) {
                result += RandomWord(random) + " ";
            }
            return result;
        }

        public static string RandomWord(Random random) {
            return WORDS[random.Next(WORDS.Length)];
        }

        public static DateTime RandomDate(Random random, DateTime start, DateTime end) {
            int range = (end - start).Days;           
            return start.AddDays(random.Next(range));
        }

        public static string RandomChars(Random random, int minlength, int maxlength) {
            string result = "";
            int length = random.Next(minlength, maxlength);
            for (int i = 0;i < length;i++) {
                result += (char) random.Next('a','z'+1);
            }
            return result;
        }

        public static Gender RandomGender(Random random) {
            return ALL_GENDER[random.Next(ALL_GENDER.Length)];
        }
    }

    public class RandomTestSeedConfig {
        public bool Enabled { get; set; }
        // initial Seed for the random instance
        public int? Seed { get; set; }
        public int Persons { get; set; }
        public int Positions { get; set; }
        public int CareerLevels { get; set; }
        public int MemberStatus { get; set; }
        public int PersonPositions { get; set; }
        public int PersonCareerLevels { get; set; }
        public int PersonMemberStatus { get; set; }
    }
}