export default function Quiz() {
  type Digimons = {
    id: number;
    name: string;
    image: string;
  };

  const digimons: Digimons[] = [
    {
      id: 1,
      name: 'Agumon',
      image: 'https://digimon-api.com/images/digimon/w/Agumon.png',
    },
    {
      id: 2,
      name: 'Patamon',
      image: 'https://digimon-api.com/images/digimon/w/Patamon.png',
    },
    {
      id: 3,
      name: 'Greymon',
      image: 'https://digimon-api.com/images/digimon/w/Greymon.png',
    },
  ];

  return (
    <>
      <p>What is the name of this digimon?</p>
      <img src={digimons[0].image} />

      <p>#1 {digimons[0].name}</p>
      <p>#1 {digimons[1].name}</p>
      <p>#1 {digimons[2].name}</p>
    </>
  );
}
