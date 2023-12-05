import { useEffect, useState } from 'react';
// import Api from '../services/Api';

export default function Quiz() {
  const [digimons, setDigimons] = useState<Digimons[]>([]);
  const [selectedDigimon, setSelectedDigimon] = useState<string | null>(null);

  const apiUrl = 'https://localhost:7285/api/Digimon';

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${apiUrl}/Questions`);
        const data = await response.json();
        setDigimons(data);
      } catch (error) {
        console.error('Error fetching digimons:', error);
      }
    };

    fetchData();
  }, []);

  type Digimons = {
    id: number;
    name: string;
    image: string;
  };

  const handleClick = (digimonName: string) => {
    console.log(`You picked ${digimonName}`);
    setSelectedDigimon(digimonName);
  };

  const digimonImage = digimons.length > 0 ? digimons[0].image : '';

  return (
    <>
      <h1>What is the name of this digimon?</h1>
      {digimonImage && (
        <img src={digimonImage} alt={`Image for ${digimons[0].name}`} />
      )}

      <ul>
        {digimons.map((digimon) => (
          <li key={digimon.id}>
            <button onClick={() => handleClick(digimon.name)}>
              {digimon.name}
            </button>
          </li>
        ))}
      </ul>
    </>
  );
}
