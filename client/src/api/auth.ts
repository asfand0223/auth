import { setSelf } from "@/redux/auth";
import { setError, setValidationErrors } from "@/redux/login";
import { AppDispatch } from "@/redux/store";
import axios from "axios";

interface ILoginParams {
  username: string;
  password: string;
}

export interface ISelf {
  user_id: string;
  username: string;
}

export const authorise = () => {
  return async (dispatch: AppDispatch) => {
    const response = await axios.get(
      `${process.env.NEXT_PUBLIC_AUTH_URL as string}/self`,
      { validateStatus: () => true, withCredentials: true },
    );
    if (response.status !== 200) return;
    dispatch(setSelf({ self: response.data }));
  };
};

export const login = ({ username, password }: ILoginParams) => {
  return async (dispatch: AppDispatch) => {
    const response = await axios.post(
      `${process.env.NEXT_PUBLIC_AUTH_URL as string}/login`,
      {
        username,
        password,
      },
      { validateStatus: () => true, withCredentials: true },
    );
    if (response.status === 200) {
      await dispatch(authorise());
    } else if (response.data.hasOwnProperty("error")) {
      dispatch(setError({ error: { message: response.data.error } }));
    } else {
      dispatch(
        setValidationErrors({
          validation_errors: response.data.validation_errors,
        }),
      );
    }
  };
};

interface IRegisterParams {
  username: string;
  password: string;
  confirm_password: string;
}

export const register = ({
  username,
  password,
  confirm_password,
}: IRegisterParams) => {
  return async (dispatch: AppDispatch) => {
    const register_response = await axios.post(
      `${process.env.NEXT_PUBLIC_AUTH_URL as string}/register`,
      {
        username,
        password,
        confirm_password,
      },
      { validateStatus: () => true, withCredentials: true },
    );

    if (register_response.status === 200) {
      await dispatch(authorise());
    } else if (register_response.data.hasOwnProperty("error")) {
      dispatch(setError({ error: { message: register_response.data.error } }));
    } else {
      dispatch(
        setValidationErrors({
          validation_errors: register_response.data.validation_errors,
        }),
      );
    }
  };
};
