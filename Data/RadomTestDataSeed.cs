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
        public static readonly Gender[] ALL_GENDER = (Gender[])Enum.GetValues(typeof(Gender));
        public static async Task DoSeed(ApplicationDbContext connection, RandomTestSeedConfig config) {
            if (!config.Enabled) {
                return;
            }
            if (config.ClearExistingData) {
                if (!config.KeepPersons) {
                    connection.Persons.RemoveRange(connection.Persons);
                }
                connection.PersonsCareerLevels.RemoveRange(connection.PersonsCareerLevels);
                connection.PersonsMemberStatus.RemoveRange(connection.PersonsMemberStatus);
                connection.PersonsPositions.RemoveRange(connection.PersonsPositions);
                connection.Positions.RemoveRange(connection.Positions);
                connection.CareerLevels.RemoveRange(connection.CareerLevels);
                connection.MemberStatus.RemoveRange(connection.MemberStatus);
                await connection.SaveChangesAsync();
                // this clears all actual data, add default data back in
                await CCDefaultDataSeed.DoSeed(connection);
            }
            Random rand;
            if (config.Seed.HasValue) {
                rand = new Random(config.Seed ?? 0);
            } else {
                rand = new Random();
            }
            DateTime start = new DateTime(1990, 1, 1);
            DateTime end = new DateTime(2018,12,21);
            for (int i = 0;i < config.Persons;i++) {
                var firstName = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 2, 6);
                var lastName = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 5, 13);
                await connection.Persons.AddAsync(new Person() {
                    AdressCity = RandomWord(rand),
                    AdressNr = rand.Next(300).ToString(),
                    AdressStreet = RandomSentence(rand, 1, 3),
                    AdressZIP = rand.Next(100000).ToString(),
                    Birthdate = RandomDate(rand, start, end),
                    EmailAssociaton = firstName.ToLower()[0] + lastName.ToLower() + "@campus-consult.org",
                    EmailPrivate = firstName.ToLower() + "." + lastName.ToLower() + "@gmail.com",
                    FirstName = firstName,
                    Gender = RandomGender(rand),
                    LastName = lastName,
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
            for (int i = 0;i < config.MemberStatus;i++) {
                await connection.MemberStatus.AddAsync(new MemberStatus() {
                    Name = RandomChars(rand, 1, 1).ToUpper() + RandomChars(rand, 2, 6),
                });
            }
            await connection.SaveChangesAsync();
            var persons = await connection.Persons
                .Include(p => p.PersonsCareerLevels)
                .Include(p => p.PersonsMemberStatus)
                .Include(p => p.PersonsPositions).ToListAsync();
            var positions = await connection.Positions.ToListAsync();
            var careerLevels = await connection.CareerLevels.ToListAsync();
            var memberStatus = await connection.MemberStatus.ToListAsync();

            if (persons.Any()) {
                if (careerLevels.Any()) {
                    for (int i = 0;i < config.PersonCareerLevels;i++) {
                        var person = PickRandom(rand, persons);
                        var careerLevel = PickRandom(rand, careerLevels);
                        // get latest career level
                        var lastLevels = person.PersonsCareerLevels.Where(level => level.End == null).ToList();
                        if (lastLevels.Count > 0) {
                            var lastLevel = lastLevels[0];
                            var lastDate = lastLevel.Begin;
                            if (lastDate > end) {
                                continue;
                            }
                            var lastEndDate = RandomDate(rand, lastDate, end);
                            lastLevel.End = lastEndDate;
                            await connection.PersonsCareerLevels.AddAsync(new PersonsCareerLevel() {
                                Begin = lastEndDate,
                                CareerLevelID = careerLevel.CareerLevelID,
                                End = null,
                                PersonID = person.PersonID,
                            });
                        } else {
                            var lastEndDate = RandomDate(rand, start, end);
                            await connection.PersonsCareerLevels.AddAsync(new PersonsCareerLevel() {
                                Begin = lastEndDate,
                                CareerLevelID = careerLevel.CareerLevelID,
                                End = null,
                                PersonID = person.PersonID,
                            });
                        }
                    }
                }

                if (memberStatus.Any()) {
                    for (int i = 0;i < config.PersonMemberStatus;i++) {
                        var person = PickRandom(rand, persons);
                        var memberStatu = PickRandom(rand, memberStatus);
                        // get latest career level
                        var lastStatus = person.PersonsMemberStatus.Where(status => status.End == null).ToList();
                        if (lastStatus.Count > 0) {
                            var lastStatu = lastStatus[0];
                            var lastDate = lastStatu.Begin;
                            if (lastDate > end) {
                                continue;
                            }
                            var lastEndDate = RandomDate(rand, lastDate, end);
                            lastStatu.End = lastEndDate;
                            await connection.PersonsMemberStatus.AddAsync(new PersonsMemberStatus() {
                                Begin = lastEndDate,
                                MemberStatusID = memberStatu.MemberStatusID,
                                End = null,
                                PersonID = person.PersonID,
                            });
                        } else {
                            var lastEndDate = RandomDate(rand, start, end);
                            await connection.PersonsMemberStatus.AddAsync(new PersonsMemberStatus() {
                                Begin = lastEndDate,
                                MemberStatusID = memberStatu.MemberStatusID,
                                End = null,
                                PersonID = person.PersonID,
                            });
                        }
                    }
                }

                if (positions.Any()) {
                    for (int i = 0;i < config.PersonPositions;i++) {
                        var person = PickRandom(rand, persons);
                        if (person.PersonsPositions.Any(p => p.End == null) && rand.Next(2) == 0) {
                            // remove a position
                            var persPos = PickRandom(rand, person.PersonsPositions.Where(p => p.End == null).ToList());
                            if (persPos.Begin > end) {
                                continue; // failsafe
                            }
                            persPos.End = RandomDate(rand, persPos.Begin, end);
                        } else {
                            // add a position
                            var pos = PickRandom(rand, positions);
                            await connection.PersonsPositions.AddAsync(new PersonsPosition() {
                                Begin = RandomDate(rand, start, end),
                                End = null,
                                PersonID = person.PersonID,
                                PositionID = pos.PositionID,
                            });
                        }
                    }
                }
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

        public static T PickRandom<T>(Random random, List<T> list) {
            return list[random.Next(list.Count)];
        }
    }

    public class RandomTestSeedConfig {
        public bool Enabled { get; set; }
        /// whether or not Persons, Positions, CareerLevels, MemberStatus, PersonPositions,
        /// PersonCareerLevels and PersonMemberStatus should be cleared before seeding test data
        public bool ClearExistingData { get; set; }
        // even if clearing existing data, keep persons
        public bool KeepPersons { get; set; }
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