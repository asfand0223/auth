"use client";
import styles from "./page.module.scss";
import Auth from "@/components/auth/auth";
import { useEffect } from "react";
import { authorise } from "@/api/auth";
import { useAppDispatch } from "@/redux/store";

const Home = () => {
  const dispatch = useAppDispatch();
  useEffect(() => {
    const authoriseAsync = async () => {
      await dispatch(authorise());
    };
    authoriseAsync();
  }, [dispatch]);
  return (
    <div className={styles.container}>
      <Auth />
    </div>
  );
};

export default Home;
