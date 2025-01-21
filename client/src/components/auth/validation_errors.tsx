import React from "react";

interface IValidationErrorsParams {
  validation_errors: Array<string>;
}

const ValidationErrors: React.FC<IValidationErrorsParams> = ({
  validation_errors,
}) => {
  return (
    <div>
      {validation_errors.map((error: string, index: number) => (
        <p key={index}>{error}</p>
      ))}
    </div>
  );
};

export default ValidationErrors;
