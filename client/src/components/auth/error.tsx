import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTriangleExclamation } from "@fortawesome/free-solid-svg-icons";
import React from "react";
import styles from "@/styles/auth/error.module.scss";

interface IErrorParams {
  error: string;
}

const Error: React.FC<IErrorParams> = ({ error }) => {
  return (
    <div className={styles.container}>
      <FontAwesomeIcon className={styles.icon} icon={faTriangleExclamation} />
      {error}
    </div>
  );
};

export default Error;
