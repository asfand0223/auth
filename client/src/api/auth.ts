import axios from "axios";

interface ILoginParams {
  username: string;
  password: string;
}

interface LoginResponse {
  id: string;
  username: string;
}

export const login = async ({
  username,
  password,
}: ILoginParams): Promise<LoginResponse> => {
  const response = await axios.post<LoginResponse>(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/login`,
    {
      username,
      password,
    },
  );

  return response.data;
};

interface IRegisterParams {
  username: string;
  password: string;
  confirm_password: string;
}

interface RegisterResponse {
  id: string;
  username: string;
}

export const register = async ({
  username,
  password,
  confirm_password,
}: IRegisterParams): Promise<RegisterResponse> => {
  const response = await axios.post<RegisterResponse>(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/register`,
    {
      username,
      password,
      confirm_password,
    },
  );

  return response.data;
};
