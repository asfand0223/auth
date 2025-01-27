# Build
FROM node:18.19.1 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Production
FROM node:18.19.1 AS production
WORKDIR /app
COPY --from=build /app /app
RUN npm install --production
EXPOSE 3000
CMD ["npm", "run", "start"]
