using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product
        {
            Id = new Guid("d1b2c3f4-5e6f-4a8b-9c0d-e1f2a3b4c5d6"),
            Name = "Laptop",
            Description = "High-performance laptop for gaming and work.",
            Price = 1200.00m,
            ImageFile = "laptop.png",
            Category = ["Electronics", "Computers"]
        },
        new Product
        {
            Id = new Guid("a1b2c3d4-e5f6-4a8b-9c0d-e1f2a3b4c5d7"),
            Name = "Smartphone",
            Description = "Latest model smartphone with advanced features.",
            Price = 800.00m,
            ImageFile = "smartphone.png",
            Category = ["Electronics", "Mobile Phones"]
        },
        new Product
        {
            Id = new Guid("f1e2d3c4-b5a6-48b9-9c0d-e1f2a3b4c5d8"),
            Name = "Headphones",
            Description = "Noise-cancelling headphones with superior sound quality.",
            Price = 200.00m,
            ImageFile = "headphones.png",
            Category = ["Electronics", "Audio"]
        },
        new Product
        {
            Id = new Guid("c1b2a3d4-e5f6-48b9-9c0d-e1f2a3b4c5d9"),
            Name = "Smartwatch",
            Description = "Stylish smartwatch with fitness tracking features.",
            Price = 250.00m,
            ImageFile = "smartwatch.png",
            Category = ["Electronics", "Wearables"]
        },
        new Product
        {
            Id = new Guid("b1c2d3e4-f5a6-48b9-0c1d-e2f3a4b5c6d7"),
            Name = "Tablet",
            Description = "Portable tablet with high-resolution display.",
            Price = 600.00m,
            ImageFile = "tablet.png",
            Category = ["Electronics", "Tablets"]
        },
        new Product
        {
            Id = new Guid("a1b2c3d4-e5f6-48b9-0c1d-e2f3a4b5c6d8"),
            Name = "Bluetooth Speaker",
            Description = "Compact Bluetooth speaker with powerful sound.",
            Price = 150.00m,
            ImageFile = "bluetooth_speaker.png",
            Category = ["Electronics", "Audio"]
        },
        new Product
        {
            Id = new Guid("f1e2d3c4-b5a6-48b9-0c1d-e2f3a4b5c6d9"),
            Name = "Gaming Console",
            Description = "Next-gen gaming console with immersive graphics.",
            Price = 500.00m,
            ImageFile = "gaming_console.png",
            Category = ["Electronics", "Gaming"]
        },
    };
}