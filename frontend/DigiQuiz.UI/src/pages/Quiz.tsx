import { useEffect, useState } from 'react';
// import Api from '../services/Api';

export default function Quiz() {
  const [digimons, setDigimons] = useState<Digimons[]>([]);
  const [selectedDigimon, setSelectedDigimon] = useState<number | null>(null);
  const [correctAnswerId, setCorrectAnswerId] = useState<number | null>(null);
  const [totalPoints, setTotalPoints] = useState<number>(0);
  const [currentImage, setCurrentImage] = useState<string>('');

  const apiUrl = 'https://localhost:7285/api/Digimon';

  // useEffect(() => {
  //   const fetchData = async () => {
  //     try {
  //       const response = await fetch(`${apiUrl}/Questions`);
  //       const data = await response.json();
  //       setDigimons(data);
  //     } catch (error) {
  //       console.error('Error fetching digimons:', error);
  //     }
  //   };

  //   fetchData();
  // }, []);

  type Digimons = {
    id: number;
    name: string;
    image: string;
  };

  const TempDigimon: Digimons[] = [
    {
      id: 1,
      name: 'Agumon',
      image: 'https://www.digi-api.com/images/digimon/w/Agumon.png',
    },
    {
      id: 2,
      name: 'Greymon',
      image: 'https://www.digi-api.com/images/digimon/w/Greymon.png',
    },
    {
      id: 3,
      name: 'Gabumon',
      image: 'https://www.digi-api.com/images/digimon/w/Gabumon.png',
    },
  ];

  useEffect(() => {
    const initRandomIndex = Math.floor(Math.random() * TempDigimon.length);
    setCurrentImage(TempDigimon[initRandomIndex].image);
    setCorrectAnswerId(TempDigimon[initRandomIndex].id);
  }, []);

  console.log(currentImage);

  const handleClick = (digimonId: number) => {
    if (digimonId === correctAnswerId) {
      const randomIndex = Math.floor(Math.random() * TempDigimon.length);
      console.log('Correct!');
      setTotalPoints(totalPoints + 5);
      setCurrentImage(TempDigimon[randomIndex].image);
      setCorrectAnswerId(TempDigimon[randomIndex].id);
    } else {
      console.log('Wrong!');
    }
  };

  return (
    <>
      <h2>Total Score: {totalPoints}</h2>
      <h1>What is the name of this digimon?</h1>
      <img src={currentImage} alt={'Image not found'} />
      {/* <ul>
        {digimons.map((digimon) => (
          <li key={digimon.id}>
          <button onClick={() => handleClick(digimon.id)}>
          {digimon.name}
          </button>
          </li>
          ))}
        </ul> */}

      <ul>
        {TempDigimon.map((digimon) => (
          <li style={{ listStyle: 'none' }} key={digimon.id}>
            <button
              onClick={() => handleClick(digimon.id)}
              style={{
                fontWeight: selectedDigimon === digimon.id ? 'bold' : 'normal',
              }}>
              {digimon.name}
            </button>
          </li>
        ))}
      </ul>
    </>
  );
}
