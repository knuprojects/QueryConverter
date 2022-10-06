namespace QueryConverter.Tests.Templates
{
    public static class QueryTemplate
    {
        // Succsesfull query
        public const string SelectWithFilters = "SELECT Name, Type, State, Pin FROM Cities WHERE Name = 'Miami' AND State = 'FL' AND ZipCodes IN(33126, 33151) AND AverageAge BETWEEN 34 AND 65AND AverageSalary >= 55230 AND AverageTemperature< 80";
        public const string SelectWithFilter = "SELECT * FROM Planets WHERE SpacecraftWithinKilometers< 10000";
        public const string SelectWithFilterAndGroupBy = "SELECT SolarSystem, Galaxy FROM Planets WHERE SpacecraftWithinKilometers< 10000 GROUP BY SolarSystem, Galaxy";
        public const string SelectWithOrderBy = "SELECT Name, Pin FROM Citites ORDER BY Name asc";

        // Invalid Query
        public const string InvalidSelectWithFilters = "SELECT Name, Type, State, Pin FDSFds Cities WHERE Name = 'Miami' AND State = 'FL' AND ZipCodes IN(33126, 33151) AND AverageAge BETWEEN 34 AND 65AND AverageSalary >= 55230 AND AverageTemperature< 80";
        public const string InvalidSelectWithFilter = "SELECT * FROM Planets WHfdsfERE SpacecraftWithinKilometers< 10000";
        public const string InvalidSelectWithFilterAndGroupBy = "SELECT SolarSystem, Galaxy FROM Planets WHERE SpacecraftWithinKilometers< 10000 GROfdsfsUP BY SolarSystem, Galaxy";
        public const string InvalidSelectWithOrderBy = "SELECT Name, Pin FROM Citites ORDER BY Name asc";
    }
}
