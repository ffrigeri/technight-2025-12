#!/bin/bash

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${BLUE}Starting TechnightApi...${NC}"

# Check if PostgreSQL is accessible
echo -e "${BLUE}Checking database connection...${NC}"

# Run migrations
echo -e "${BLUE}Applying database migrations...${NC}"
dotnet ef database update

if [ $? -eq 0 ]; then
    echo -e "${GREEN}✓ Database migrations applied successfully${NC}"
else
    echo -e "${RED}✗ Failed to apply migrations. Please check your database connection.${NC}"
    echo -e "${RED}  Connection string should be set in appsettings.json or environment variable${NC}"
    exit 1
fi

# Run the application
echo -e "${BLUE}Starting the API server...${NC}"
dotnet run

