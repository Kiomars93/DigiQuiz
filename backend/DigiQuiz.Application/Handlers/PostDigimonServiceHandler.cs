using DigiQuiz.Application.Commands;
using DigiQuiz.Application.Interfaces;
using DigiQuiz.Domain.Entities;
using MediatR;

namespace DigiQuiz.Application.Handlers;

public class PostDigimonServiceHandler : IRequestHandler<PostDigimonServiceCommand, Player>
{
    private readonly IDigimonRepository _digimonRepository;

    public PostDigimonServiceHandler(IDigimonRepository digimonRepository)
    {
        _digimonRepository = digimonRepository;
    }

    // Todo: ta bort allt med player. Här ska digimon logiken skötas att om du svara rätt så får du poäng annars inget
    // Får returnera poängen till frontend:en så att det skjuts in till player table
    public async Task<Player> Handle(PostDigimonServiceCommand request, CancellationToken cancellationToken)
    {

        //DTO:n ska vara i frontend och det skickas ner och det är ju baserat init get

        //Skickar ner en lista med DTO

        //Sen logik för och rätt och fel
        // totalpoäng

        if (request == null)
        {

        }

        var testPlayer = new Player();



        if (request == null) { }

        
        
        // om digiinput's id matchar någon id från listan då läggs det till totalpoints


        return testPlayer;
    }

    //Todo: PostDigimon metoden det ska inte vara kvar här:

    
}
