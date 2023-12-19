export const fetchData = async (url: string) => {
  const response = await fetch(url);
  const data = await response.json();
  return data;
};

export const PostData = async (url: string, requestOptions: RequestInit) => {
  const response = await fetch(url, requestOptions);
  const data = await response.json();
  console.log(data);
  return data;
};
