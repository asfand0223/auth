import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export enum AuthType {
  Login = 0,
  Register = 1,
}

interface IInitialState {
  access_token: string | null;
  auth_type: AuthType;
}

const initialState: IInitialState = {
  access_token: null,
  auth_type: AuthType.Login,
};

interface ISetAuthType {
  auth_type: AuthType;
}

export const auth_slice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuthType(state: IInitialState, action: PayloadAction<ISetAuthType>) {
      state.auth_type = action.payload.auth_type;
    },
  },
});

export const { setAuthType } = auth_slice.actions;
export const auth_reducer = auth_slice.reducer;
