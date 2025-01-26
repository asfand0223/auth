import { ILoginValidationErrors } from "@/redux/login";
import { IRegisterValidationErrors } from "@/redux/register";
import axios from "axios";

interface ILoginParams {
  username: string;
  password: string;
}

export interface ILoginResponse {
  validation_errors: ILoginValidationErrors | null;
  error: string | null;
  status: number;
}

export const login = async ({
  username,
  password,
}: ILoginParams): Promise<ILoginResponse> => {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/login`,
    {
      username,
      password,
    },
    { validateStatus: () => true, withCredentials: true },
  );
  var res = { validation_errors: null, error: null, status: response.status };
  if (response.status == 200) return res;
  if (response.data.hasOwnProperty("error")) {
    res.error = response.data.error;
  } else {
    res.validation_errors = response.data;
  }
  return res;
};

interface IRegisterParams {
  username: string;
  password: string;
  confirm_password: string;
}

export interface IRegisterResponse {
  validation_errors: IRegisterValidationErrors | null;
  error: string | null;
  status: number;
}

export const register = async ({
  username,
  password,
  confirm_password,
}: IRegisterParams): Promise<IRegisterResponse> => {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_AUTH_URL as string}/register`,
    {
      username,
      password,
      confirm_password,
    },
    { validateStatus: () => true, withCredentials: true },
  );
  var res = { validation_errors: null, error: null, status: response.status };
  if (response.status == 200) return res;
  if (response.data.hasOwnProperty("error")) {
    res.error = response.data.error;
  } else {
    res.validation_errors = response.data;
  }
  return res;
};
