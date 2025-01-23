import axios, { AxiosResponse } from "axios";

interface ILoginParams {
  username: string;
  password: string;
}

export interface ILoginResponse {
  data: string;
  error: string;
}

export interface ILoginData {
  access_token: string;
}

export const login = async ({
  username,
  password,
}: ILoginParams): Promise<AxiosResponse<any, any>> => {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/login`,
    {
      username,
      password,
    },
    { validateStatus: () => true },
  );
  return response;
};

interface IRegisterParams {
  username: string;
  password: string;
  confirm_password: string;
}

export interface IRegisterResponse {
  data: string;
  error: string;
}

export interface IRegisterData {
  access_token: string;
}

export const register = async ({
  username,
  password,
  confirm_password,
}: IRegisterParams): Promise<AxiosResponse<any, any>> => {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/register`,
    {
      username,
      password,
      confirm_password,
    },
    { validateStatus: () => true },
  );

  return response;
};
