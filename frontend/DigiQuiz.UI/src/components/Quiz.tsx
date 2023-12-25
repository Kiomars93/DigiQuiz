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
  const [displayedQuestionIds, setDisplayedQuestionIds] = useState<number[]>(
    []
  );
  const [correctAnswerId, setCorrectAnswerId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [currentImage, setCurrentImage] = useState<string>('');
  const [buttonClasses, setButtonClasses] = useState<string[]>(
    Array(digimonsQuestion.length).fill('')
  );

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
        throw new Error(`HTTP error! Status: ${error}`);
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
    setCorrectAnswerId(null);
    setButtonClasses(Array(digimonsQuestion.length).fill(''));
    const remainingQuestions = digimonsData.filter(
      (digimon) => !displayedQuestionIds.includes(digimon.id)
    );

    const randomThreeItems = getRandomThreeItems(remainingQuestions);

    setDigimonsQuestion(randomThreeItems);

    setDisplayedQuestionIds((prevIds) => [
      ...prevIds,
      ...randomThreeItems.map((item) => item.id),
    ]);
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

  const handleAnswersClick = (digimonId: number, index: number) => {
    const randomIndex = Math.floor(Math.random() * digimonsQuestion.length);
    setCorrectAnswerId(digimonsQuestion[randomIndex].id);

    if (digimonId === correctAnswerId) {
      updateTotalPointsState(totalPointsprops + 5);
      setButtonClasses((prevClasses) => {
        const newClasses = [...prevClasses];
        newClasses[index] = 'correct';
        return newClasses;
      });
    } else {
      setButtonClasses((prevClasses) => {
        const newClasses = [...prevClasses];
        newClasses[index] = 'wrong';
        return newClasses;
      });
    }

    if (currentPage === 10) {
      navigate('/scoreboard');
    } else {
      setTimeout(() => {
        setCurrentPage((prevPage) => maxPageState(prevPage + 1));
        setCurrentImage(digimonsQuestion[randomIndex].image);
        updateQuestionRound();
      }, 1000);
    }
  };

  const maxPageState = (newState: number) => {
    return Math.min(newState, 10);
  };
  return (
    <div className='digimon'>
      <h1 className='visually-hidden'>Digimon QUIZ Game</h1>
      <p className='total-score'>Total Score: {totalPointsprops}</p>
      <p className='current-question'>Question: {currentPage}/10</p>
      <h2>What is the name of this digimon?</h2>
      <img
        src={currentImage}
        alt={`Image not found`}
        className='digimon-image'
      />
      <ul className='answer-list'>
        {digimonsQuestion.map((digimon, index) => (
          <li className='list-item' key={digimon.id}>
            <button
              id={`button-${digimon.id}`}
              type='button'
              onClick={() => handleAnswersClick(digimon.id, index)}
              className={`answer-button ${buttonClasses[index]}`}>
              {digimon.name}
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
