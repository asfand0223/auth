import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export enum AuthType {
  Login = 0,
  Register = 1,
}

export interface User {
  id: string;
  username: string;
  access_token: string;
}

interface IInitialState {
  user: User | null;
  auth_type: AuthType;
}

const initialState: IInitialState = {
  user: null,
  auth_type: AuthType.Login,
};

interface ISetAuthType {
  auth_type: AuthType;
}

interface ISetUser {
  user: User;
}

export const auth_slice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setUser(state: IInitialState, action: PayloadAction<ISetUser>) {
      state.user = action.payload.user;
    },
    setAuthType(state: IInitialState, action: PayloadAction<ISetAuthType>) {
      state.auth_type = action.payload.auth_type;
    },
  },
});

export const { setUser, setAuthType } = auth_slice.actions;
export const auth_reducer = auth_slice.reducer;
