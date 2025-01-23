"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import styles from "@/styles/auth/welcome.module.scss";

const Welcome = () => {
  const { access_token } = useSelector((state: RootState) => state.auth);
  return <div className={styles.container}>{access_token}</div>;
};

export default Welcome;
