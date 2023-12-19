import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { fetchData } from '../services/APIHelper';
import { useEffect, useState } from 'react';

export default function Leaderboard() {
  const [leaderboard, setLeaderboard] = useState<LeaderBoard[]>([]);

  type LeaderBoard = {
    id: number;
    name: string;
    points: number;
    gameDate: string;
    rank: number;
  };
  const baseUrl = 'https://localhost:7285/api/Digimon';

  useEffect(() => {
    const fetchDataAsync = async (url: string) => {
      try {
        const result: LeaderBoard[] = await fetchData(url);

        const updatedResultData = result.map((player, index) => ({
          ...player,
          rank: index + 1,
        }));

        setLeaderboard(updatedResultData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchDataAsync(`${baseUrl}/leaderboard`);
  }, []);

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label='simple table'>
          <TableHead>
            <TableRow>
              <TableCell>Rank</TableCell>
              <TableCell align='right'>Players</TableCell>
              <TableCell align='right'>Points</TableCell>
              <TableCell align='right'>Game Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {leaderboard.map((row) => (
              <TableRow
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                <TableCell component='th' scope='row'>
                  {row.rank}
                </TableCell>
                <TableCell align='right'>{row.name}</TableCell>
                <TableCell align='right'>{row.points}</TableCell>
                <TableCell align='right'>{row.gameDate}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
