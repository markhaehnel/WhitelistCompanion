import * as React from "react";
import { Logo } from "./components/Logo";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { InfoText } from "./components/InfoText";
import { WhitelistAddCard } from "./components/WhitelistAddCard";
import { AuthContainer } from "./components/AuthContainer";
import { PlayerList } from "./components/PlayerList";
import { Box, GridItem, SimpleGrid, Spacer } from "@chakra-ui/layout";
import { MapCard } from "./components/MapCard";

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            refetchInterval: 10000,
        },
    },
});

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <Logo />
            <Box minH={8} />
            <AuthContainer>
                <InfoText />
                <SimpleGrid
                    columns={{ base: 1, md: 2 }}
                    spacing={4}
                    maxW="full"
                    w={{ base: "full", md: "container.lg" }}
                    p={{ base: 0, md: 4 }}
                >
                    <GridItem overflow="auto">
                        <WhitelistAddCard />
                        <Spacer h={4} />
                        <WhitelistList />
                    </GridItem>
                    <GridItem>
                        <PlayerList />
                        <Spacer h={4} />
                        <MapCard />
                    </GridItem>
                </SimpleGrid>
            </AuthContainer>
        </QueryClientProvider>
    );
}

export default App;
