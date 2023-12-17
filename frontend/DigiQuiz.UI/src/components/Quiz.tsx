import { useEffect, useState } from 'react';
import { fetchData } from '../services/APIHelper';
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
  const [digimons, setDigimons] = useState<Digimons[] | null>([]);
  const [correctAnswerId, setCorrectAnswerId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [currentImage, setCurrentImage] = useState<string>('');

  const baseUrl = 'https://localhost:7285/api/Digimon';

  type Digimons = {
    id: number;
    name: string;
    image: string;
  };

  useEffect(() => {
    const fetchDataAsync = async (url: string) => {
      try {
        const result: Digimons[] = await fetchData(url);
        setDigimons(result);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchDataAsync(`${baseUrl}/Questions`);
  }, []);

  if (digimons === null || digimons === undefined) {
    return <p>Loading...</p>;
  }

  const initRandomIndex = Math.floor(Math.random() * digimons.length);
  useEffect(() => {
    if (digimons.length > 0) {
      setCurrentImage(digimons[initRandomIndex].image || '');
      setCorrectAnswerId(digimons[initRandomIndex].id);
    }
  }, [digimons]);

  const handleAnswersClick = (digimonId: number) => {
    if (currentPage === 10) {
      alert('End game');
      navigate('/scoreboard');
    }

    if (digimonId === correctAnswerId) {
      const randomIndex = Math.floor(Math.random() * digimons.length);
      console.log('Correct!');
      updateTotalPointsState(totalPointsprops + 5);
      setCurrentImage(digimons[randomIndex].image);
      setCorrectAnswerId(digimons[randomIndex].id);
    } else {
      console.log('Wrong!');
    }
    setCurrentPage(currentPage + 1);
    console.log(currentPage);
  };

  console.log(digimons);

  return (
    <>
      <h2>Total Score: {totalPointsprops}</h2>
      <h2>{currentPage}/10</h2>
      <h1>What is the name of this digimon?</h1>

      <img src={currentImage} alt={`Image not found`} />
      <ul>
        {digimons.map((digimon) => (
          <li style={{ listStyle: 'none' }} key={digimon.id}>
            <button
              onClick={() => handleAnswersClick(digimon.id)}
              style={{
                // fontWeight: digimons === digimon.id ? 'bold' : 'normal',
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
