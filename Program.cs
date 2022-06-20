using System;
using System.Linq;
using LINQtoCSV;
using System.Collections.Generic;

namespace FamilyGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            readCsvFiles();
            Console.ReadKey();
        }
        private static void showTotalFamilyMemebrs(List<people> people, List<relations> relations)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------- Total Family Members ------------------------------");
            Console.WriteLine();

            int count = 1;
            List<string> repeatedRecords = new List<string>();
            List<string> storeFamilyCount = new List<string>();



            foreach(var person1 in relations)
            {
                foreach(var person2 in relations)
                {
                    if(person1.email == person2.email && !repeatedRecords.Contains(person2.email) && person1.relationship == "FAMILY")
                    {
                        count++;
                        repeatedRecords.Add(person2.emailOfRelative);
                    }
                    else if(person1.email != person2.email && !repeatedRecords.Contains(person2.email) && repeatedRecords.Contains(person2.emailOfRelative) && person1.relationship == "FAMILY")
                    {
                        count++;
                        repeatedRecords.Add(person2.email);
                    }
                }
                if(!storeFamilyCount.Contains(person1.email) && !repeatedRecords.Contains(person1.email))
                {
                    storeFamilyCount.Add(person1.email);
                    storeFamilyCount.Add(count.ToString());
                }
                count = 1;
            } 

            for(int i = 0; i < storeFamilyCount.Count; i += 2)
            {
                foreach(var checkName in people)
                {
                    if(storeFamilyCount[i] == checkName.email)
                    {
                        Console.WriteLine(checkName.name + " : " + storeFamilyCount[i + 1]);
                    }
                }
            }

        }

        private static void showTotalRelations(List<people> people, List<relations> relations)
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------- Total Relations With Each Other ------------------------------");
            Console.WriteLine();
            List<string> storeCount = new List<string>();
            int count = 0;
            foreach(var person in people)
            {
                foreach(var relative in relations)
                {
                    if(person.email == relative.email || person.email == relative.emailOfRelative)
                    {
                        count++;
                    }
                    else if (person.email != relative.email && relative.relationship != "FAMILY" && relative.relationship != "FRIEND")
                    {
                        count = 0;
                    }
                }
                storeCount.Add(person.name);
                storeCount.Add(count.ToString());
                count = 0;
            }

            for (int i = 0; i < storeCount.Count; i += 2)
            {
                Console.WriteLine(storeCount[i] +" : "+storeCount[i+1]);
            }
        }

        private static void checkRelations(List<people> people, List<relations> relations)
        {
            List<relationsMap> storeRelations = new List<relationsMap>();
            
            foreach (var person in people)
            {
                foreach(var relative in relations)
                {
                    foreach(var person1 in people)
                    {
                        if (person.email == relative.email && relative.emailOfRelative == person1.email)
                        {
                            Console.WriteLine(person.name + " and " + person1.name + " are " + relative.relationship);
                        }
                    }
                }
            }
            
        }

        private static void readCsvFiles()
        {
            var csvFile = new CsvFileDescription
            {
                FirstLineHasColumnNames = false,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = true,
                EnforceCsvColumnAttribute = true
            };

            var csvContext = new CsvContext();

            List<people> peopleCsv = csvContext.Read<people>("people.csv", csvFile).ToList();
            List<relations> relationshipsCsv = csvContext.Read<relations>("relationships.csv", csvFile).ToList();

            checkRelations(peopleCsv,relationshipsCsv);
            showTotalRelations(peopleCsv, relationshipsCsv);
            showTotalFamilyMemebrs(peopleCsv, relationshipsCsv);
        }
    }
}
