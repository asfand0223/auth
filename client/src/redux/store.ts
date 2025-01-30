import { configureStore, ThunkDispatch, UnknownAction } from "@reduxjs/toolkit";
import { auth_reducer } from "./auth";
import { login_reducer } from "./login";
import { register_reducer } from "./register";
import { useDispatch } from "react-redux";

export const store = configureStore({
  reducer: {
    auth: auth_reducer,
    login: login_reducer,
    register: register_reducer,
  },
  devTools: process.env.NODE_ENV !== "production",
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = ThunkDispatch<RootState, unknown, UnknownAction>;
export const useAppDispatch = () => useDispatch<AppDispatch>();
