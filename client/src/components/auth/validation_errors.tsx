import { faCircleExclamation } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import styles from "@/styles/auth/validation_errors.module.scss";

interface IValidationErrorsParams {
  validation_errors: Array<string>;
}

const ValidationErrors: React.FC<IValidationErrorsParams> = ({
  validation_errors,
}) => {
  return (
    <div className={styles.container}>
      {validation_errors.map((error: string, index: number) => (
        <p key={index}>
          <FontAwesomeIcon className={styles.icon} icon={faCircleExclamation} />
          {error}
        </p>
      ))}
    </div>
  );
};

export default ValidationErrors;
