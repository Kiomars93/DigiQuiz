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
  const [digimonsData, setDigimonsData] = useState<Digimons[] | null>([]);
  const [digimonsQuestion, setDigimonsQuestion] = useState<Digimons[]>([]);
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
        setDigimonsData(result);
        updateTotalPointsState(0);
        setCurrentPage(1);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchDataAsync(`${baseUrl}/Questions`);
  }, []);

  //Todo: null check:a bättre
  if (digimonsData === null || digimonsData === undefined) {
    console.log('brb');
    return <p>Loading...</p>;
  }

  useEffect(() => {
    if (digimonsData.length > 0 && digimonsQuestion.length < 3) {
      updateQuestionRound();
    }
  }, [digimonsData, digimonsQuestion]);

  const updateQuestionRound = () => {
    const remainingQuestions = digimonsData.filter(
      (digimon) =>
        !digimonsQuestion.some((selectedItem) => selectedItem.id === digimon.id)
    );

    const randomThreeItems = getRandomThreeItems(remainingQuestions);
    setDigimonsQuestion([...randomThreeItems]);
  };

  const getRandomThreeItems = (items: Digimons[]): Digimons[] => {
    const shuffledItems = [...items].sort(() => Math.random() - 0.5);
    return shuffledItems.slice(0, 3);
  };

  const initRandomIndex = Math.floor(Math.random() * digimonsQuestion.length);

  useEffect(() => {
    if (digimonsQuestion.length > 0) {
      setCurrentImage(digimonsQuestion[initRandomIndex].image || '');
      setCorrectAnswerId(digimonsQuestion[initRandomIndex].id);
    }
  }, [digimonsQuestion]);

  const handleAnswersClick = (digimonId: number) => {
    const randomIndex = Math.floor(Math.random() * digimonsQuestion.length);
    setCurrentImage(digimonsQuestion[randomIndex].image);
    setCorrectAnswerId(digimonsQuestion[randomIndex].id);

    if (digimonId === correctAnswerId) {
      updateTotalPointsState(totalPointsprops + 5);
    }

    setCurrentPage((prevPage) => maxPageState(prevPage + 1));

    if (currentPage === 10) {
      navigate('/scoreboard');
    } else {
      updateQuestionRound();
    }
  };

  const maxPageState = (newState: number) => {
    return Math.min(newState, 10);
  };

  return (
    <>
      <h2>Total Score: {totalPointsprops}</h2>
      <h2>{currentPage}/10</h2>
      <h1>What is the name of this digimon?</h1>

      <img
        height='300'
        width='300'
        src={currentImage}
        alt={`Image not found`}
        style={{ borderRadius: '5%', border: 'solid' }}
      />
      <ul>
        {digimonsQuestion.map((digimon) => (
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
