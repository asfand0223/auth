import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

interface IInitialState {
  username: string;
  password: string;
}

const initialState: IInitialState = {
  username: "",
  password: "",
};

interface ISetUsername {
  username: string;
}

interface ISetPassword {
  password: string;
}

export const login_slice = createSlice({
  name: "login",
  initialState,
  reducers: {
    setUsername(state: IInitialState, action: PayloadAction<ISetUsername>) {
      state.username = action.payload.username;
    },
    setPassword(state: IInitialState, action: PayloadAction<ISetPassword>) {
      state.password = action.payload.password;
    },
  },
});

export const { setUsername, setPassword } = login_slice.actions;
export const login_reducer = login_slice.reducer;
