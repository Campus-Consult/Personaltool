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
    public class CCDefaultDataSeed {
        public static async Task DoSeed(ApplicationDbContext connection) {
            if (!await connection.CareerLevels.AnyAsync()) {
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Trainee",
                    ShortName = "T"
                });
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Associate",
                    ShortName = "A"
                });
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Junior Consultant",
                    ShortName = "J"
                });
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Consultant",
                    ShortName = "C"
                });
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Senior Consultant",
                    ShortName = "S"
                });
                await connection.CareerLevels.AddAsync(new CareerLevel() {
                    IsActive = true,
                    Name = "Partner",
                    ShortName = "P"
                });
            }

            if (!await connection.Positions.AnyAsync()) {
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "1. Vorsitzende/r",
                    ShortName = "1V",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "2. Vorsitzende/r",
                    ShortName = "2V",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Vorstand Unternehmenskontakte",
                    ShortName = "V-UK",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Vorstand Personal",
                    ShortName = "V-P",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Vorstand Finanzen & Recht",
                    ShortName = "V-F&R",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Vorstand Qualität & Organisation",
                    ShortName = "V-Q&O",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Netzwerkbeauftragte/r",
                    ShortName = "NB",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Projektcontroller",
                    ShortName = "PC",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Schulungsbeauftragte/r",
                    ShortName = "SB",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Social-Media-Beauftragte/r",
                    ShortName = "1V",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Alkohol",
                    ShortName = "RL-C2H5OH",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Vertrieb",
                    ShortName = "RL-V",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Human Resources",
                    ShortName = "RL-HR",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Finanzen & Recht",
                    ShortName = "RL-F&R",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Qualitätsmanagement",
                    ShortName = "RL-QM",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Marketing & Public Relations",
                    ShortName = "RL-M/PR",
                });
                await connection.Positions.AddAsync(new Position() {
                    IsActive = true,
                    Name = "Ressortleitung Information Technologies",
                    ShortName = "RL-IT",
                });
            }

            if (!await connection.MemberStatus.AnyAsync()) {
                await connection.MemberStatus.AddAsync(new MemberStatus() {
                    Name = "Anwärter/in",
                });
                await connection.MemberStatus.AddAsync(new MemberStatus() {
                    Name = "Aktives Mitglied",
                });
                await connection.MemberStatus.AddAsync(new MemberStatus() {
                    Name = "Passives Mitglied",
                });
                await connection.MemberStatus.AddAsync(new MemberStatus() {
                    Name = "Ehemalige/r",
                });
            }
            await connection.SaveChangesAsync();
        }
    }
}