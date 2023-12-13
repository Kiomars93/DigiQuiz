import { useEffect, useState } from 'react';
// import Api from '../services/Api';
import { useNavigate } from 'react-router-dom';

interface TotalPointProps {
  totalPointsprops: number;
  updateTotalPointsState: (newState: number) => void;
}

export default function Quiz({
  totalPointsprops,
  updateTotalPointsState,
}: TotalPointProps) {
  const navigate = useNavigate();
  const [digimons, setDigimons] = useState<Digimons[]>([]);
  const [selectedDigimon, setSelectedDigimon] = useState<number | null>(null);
  const [correctAnswerId, setCorrectAnswerId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
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

  const handleAnswersClick = (digimonId: number) => {
    if (currentPage === 10) {
      alert('End game');
      navigate('/scoreboard');
    }

    if (digimonId === correctAnswerId) {
      const randomIndex = Math.floor(Math.random() * TempDigimon.length);
      console.log('Correct!');
      updateTotalPointsState(totalPointsprops + 5);
      setCurrentImage(TempDigimon[randomIndex].image);
      setCorrectAnswerId(TempDigimon[randomIndex].id);
    } else {
      console.log('Wrong!');
    }
    setCurrentPage(currentPage + 1);
    console.log(currentPage);
  };

  return (
    <>
      <h2>Total Score: {totalPointsprops}</h2>
      <h2>{currentPage}/10</h2>
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
              onClick={() => handleAnswersClick(digimon.id)}
              style={{
                fontWeight: selectedDigimon === digimon.id ? 'bold' : 'normal',
                backgroundColor: 'aqua',
              }}>
              {digimon.name}
            </button>
          </li>
        ))}
      </ul>
    </>
  );
}
