using Sprint_4.Data;
using Sprint_4.Models;
using Sprint_4.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Sprint_4.Tests.Services;

public class MotoServiceTests
{
    private MotoService CreateService()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var ctx = new AppDbContext(options);
        return new MotoService(ctx);
    }

    [Fact]
    public async Task AddAsync_PersistsMoto()
    {
        var service = CreateService();
        var moto = new Moto { Marca = "Honda", Modelo = "CG", Ano = 2020 };
        var created = await service.AddAsync(moto);
        Assert.NotEqual(0, created.Id);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesFields()
    {
        var service = CreateService();
        var moto = await service.AddAsync(new Moto { Marca = "Honda", Modelo = "CG", Ano = 2020 });
        moto.Modelo = "Titan";
        var ok = await service.UpdateAsync(moto.Id, moto);
        Assert.True(ok);
        var loaded = await service.GetByIdAsync(moto.Id);
        Assert.Equal("Titan", loaded!.Modelo);
    }

    [Fact]
    public async Task DeleteAsync_Removes()
    {
        var service = CreateService();
        var moto = await service.AddAsync(new Moto { Marca = "Yamaha", Modelo = "Factor", Ano = 2021 });
        var removed = await service.DeleteAsync(moto.Id);
        Assert.True(removed);
        var loaded = await service.GetByIdAsync(moto.Id);
        Assert.Null(loaded);
    }
}
