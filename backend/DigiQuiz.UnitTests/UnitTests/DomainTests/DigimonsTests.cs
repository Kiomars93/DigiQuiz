using DigiQuiz.Domain.Models;
using Moq;

namespace DigiQuiz.Tests.UnitTests.DomainTests;

public class DigimonsTests
{
    //var digimonsMock = new Mock<Digimons>();
    //digimonsMock.Setup(d => d.Contents).Returns(digimons.Contents);
    [Fact]
    public void IsValid_ComparingDigimonsContentValues_ShouldHaveCorrectValues()
    {
        // Arrange
        var expectedId = 1;
        var expectedName = "Agumon";
        var expectedImage = "https://www.digi-api.com/images/digimon/w/Agumon.png";

        // Act
        var digimons = new Digimons
        {
            Contents = new List<ContentObj> { new ContentObj
            {
                Id = expectedId,
                Name = expectedName,
                Image = expectedImage
            }}
        };

        // Assert
        Assert.Equal(expectedId, digimons.Contents[0].Id);
        Assert.Equal(expectedName, digimons.Contents[0].Name);
        Assert.Equal(expectedImage, digimons.Contents[0].Image);
    }
}