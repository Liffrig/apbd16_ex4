using apbd16_ex4.Model;

namespace apbd16_ex4.Data;

public static class Database {
    public static List<Animal> Animals { get; set; } = [
        new Animal { Id = 1, Name = "Diego", Category = "Pies", Weight = 15.5, FurColor = "Rudy" },
        new Animal { Id = 2, Name = "Hiro", Category = "Kot", Weight = 4.2, FurColor = "Szary" },
        new Animal { Id = 3, Name = "Behemot", Category = "Szczur", Weight = 8.7, FurColor = "Biały" }
    ];

    public static List<Visit> Visits { get; set; } = [
        new Visit {
            Id = 1, AnimalId = 1, VisitDate = DateTime.Now.AddDays(-10), Description = "Szczepienie", Price = 150.00m
        },
        new Visit {
            Id = 2, AnimalId = 2, VisitDate = DateTime.Now.AddDays(-5), Description = "Uśpienie", Price = 80.00m
        },
        new Visit {
            Id = 3, AnimalId = 1, VisitDate = DateTime.Now.AddDays(-2), Description = "Kontrola", Price = 70.00m
        }
    ];

    private static int _lastAnimalId = 3;
    private static int _lastVisitId = 3;

    public static int GetNextAnimalId() {
        return ++_lastAnimalId;
    }

    public static int GetNextVisitId() {
        return ++_lastVisitId;
    }
}
    
