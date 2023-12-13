import React from 'react';

interface TotalPointProps {
  totalPointsprops: number;
}

export default function Scoreboard({ totalPointsprops }: TotalPointProps) {
  return (
    <>
      <h1>Great Job!</h1>
      <h2>High score:</h2>
      <label>Name:</label>
      <input type='text' required placeholder='Name...' />
      <p>Total points: {totalPointsprops}</p>
      <input type='submit' />
    </>
  );
}
