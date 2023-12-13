import Quiz from './pages/Quiz';
import './App.css';
import Scoreboard from './pages/Scoreboard';
import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';

function App() {
  const [totalPoints, setTotalPoints] = useState<number>(0);

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
      </Routes>
    </>
  );
}

export default App;
