"use client";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import Welcome from "@/components/auth/welcome";
import styles from "./page.module.scss";
import Auth from "@/components/auth/auth";

const Home = () => {
  const { user } = useSelector((state: RootState) => state.auth);
  return (
    <div className={styles.container}>
      {user && <Welcome />}
      {!user && <Auth />}
    </div>
  );
};

export default Home;
