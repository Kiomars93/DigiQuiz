import Quiz from './components/Quiz';
import './App.css';
import Scoreboard from './components/Scoreboard';
import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import Leaderboard from './components/Leaderboard';

function App() {
  const [totalPoints, setTotalPoints] = useState<number>(1);

  const updateTotalPointsState = (newState: number) => {
    setTotalPoints(newState);
  };

  return (
    <>
      <Routes>
        <Route
          path='/'
          element={
            <Quiz
              totalPointsprops={totalPoints}
              updateTotalPointsState={updateTotalPointsState}
            />
          }
        />
        <Route
          path='/scoreboard'
          element={<Scoreboard totalPointsprops={totalPoints} />}
        />
        <Route path='/leaderboard' element={<Leaderboard />} />
      </Routes>
    </>
  );
}

export default App;
