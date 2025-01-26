"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import styles from "@/styles/auth/welcome.module.scss";

const Welcome = () => {
  const { self } = useSelector((state: RootState) => state.auth);
  return <div className={styles.container}>Welcome {self?.username}</div>;
};

export default Welcome;
