import React from "react";
import { Logo } from "./components/Logo";
import { WhitelistList } from "./components/WhitelistList";
import { QueryClient, QueryClientProvider } from "react-query";
import { InfoText } from "./components/InfoText";
import { WhitelistAddCard } from "./components/WhitelistAddCard";
import { AuthContainer } from "./components/AuthContainer";

const queryClient = new QueryClient();

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <Logo />
            <AuthContainer>
                <>
                    <InfoText />
                    <WhitelistAddCard />
                    <WhitelistList />
                </>
            </AuthContainer>
        </QueryClientProvider>
    );
}

export default App;
