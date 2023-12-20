import { useEffect, useState } from 'react';
import { PostData, fetchData } from '../services/APIHelper';
import { useNavigate } from 'react-router-dom';

interface TotalPointProps {
  totalPointsprops: number;
}

type Players = {
  name: string;
  points: number;
};

export default function Scoreboard({ totalPointsprops }: TotalPointProps) {
  const navigate = useNavigate();
  const [playerName, setPlayerName] = useState<string>('');
  const [playerData, setPlayerData] = useState<Players | null>(null);

  const baseUrl = 'https://localhost:7285/api/Digimon';
  const requestOptions: RequestInit = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      name: playerName,
      points: totalPointsprops,
    }),
  };

  const PostDataAsync = async (url: string) => {
    try {
      const result: Players = await PostData(url, requestOptions);
      setPlayerData(result);
      console.log(result);

      return result;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (playerName.trim() != '') {
      try {
        const result = await PostDataAsync(`${baseUrl}/Scoreboard`);

        navigate('/leaderboard');
      } catch (error) {
        console.error('Error posting data:', error);
      }
    }
  };

  return (
    <>
      <h1>Great Job!</h1>
      <h2>High score:</h2>
      <p>Total points: {totalPointsprops}</p>
      <form onSubmit={handleSubmit}>
        <input
          placeholder='Enter your name'
          type='text'
          value={playerName}
          required
          onChange={(e) => setPlayerName(e.target.value)}
        />
        <button type='submit'>Submit</button>
      </form>
    </>
  );
}
