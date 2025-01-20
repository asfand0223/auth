"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import styles from "@/styles/auth/welcome.module.scss";

const Welcome = () => {
  const { user } = useSelector((state: RootState) => state.auth);
  return (
    <div className={styles.container}>
      {user && <p>{`Hello ${user.username}`}</p>}
    </div>
  );
};

export default Welcome;
