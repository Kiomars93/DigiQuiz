import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default function Leaderboard() {
  function createData(
    id: number,
    name: string,
    points: number,
    gameDate: Date
  ) {
    return { id, name, points, gameDate };
  }
  let date: Date = new Date('2023-12-14');

  const rows = [
    createData(1, 'Frozen yoghurt', 6.0, date),
    createData(2, 'Frozen yoghurt', 6.0, date),
    createData(3, 'Frozen yoghurt', 6.0, date),
    createData(4, 'Frozen yoghurt', 6.0, date),
    createData(5, 'Frozen yoghurt', 6.0, date),
    createData(6, 'Frozen yoghurt', 6.0, date),
    createData(7, 'Frozen yoghurt', 6.0, date),
    createData(8, 'Frozen yoghurt', 6.0, date),
    createData(9, 'Frozen yoghurt', 6.0, date),
    createData(10, 'Frozen yoghurt', 6.0, date),
  ];

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label='simple table'>
          <TableHead>
            <TableRow>
              <TableCell>Rank</TableCell>
              <TableCell align='right'>Player</TableCell>
              <TableCell align='right'>Points</TableCell>
              <TableCell align='right'>Game Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
              <TableRow
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                <TableCell component='th' scope='row'>
                  {row.name}
                </TableCell>
                <TableCell align='right'>{row.name}</TableCell>
                <TableCell align='right'>{row.points}</TableCell>
                <TableCell align='right'>
                  {row.gameDate.toDateString()}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
