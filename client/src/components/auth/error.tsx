import React from "react";

interface IErrorParams {
  error: string;
}

const Error: React.FC<IErrorParams> = ({ error }) => {
  return <div>{error}</div>;
};

export default Error;
