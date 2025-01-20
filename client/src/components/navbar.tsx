import React from "react";
import styles from "../styles/navbar.module.scss";

const Navbar: React.FC = () => {
  return (
    <div className={styles.container}>
      <p className={styles.title}>Auth</p>
    </div>
  );
};

export default Navbar;
