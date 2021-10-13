import * as React from "react";
import { useQuery } from "react-query";
import { fetchUserList } from "../api";
import { Card } from "./Card";
import SimpleBar from "simplebar-react";
import { Spinner } from "@chakra-ui/spinner";
import { Box, Flex } from "@chakra-ui/layout";
import { Spacer, Text, useColorModeValue } from "@chakra-ui/react";
import { ScrollableListContent } from "./ScrollableListContent";

export function PlayerList() {
    const {
        data: data,
        error,
        isFetching,
    } = useQuery("userlist", async () => {
        let data = await fetchUserList();
        data.data.users = data?.data?.users.sort((a: string, b: string) =>
            a.toLowerCase().localeCompare(b.toLowerCase())
        );
        return data?.data;
    });

    return (
        <Card>
            <Flex p={4}>
                <Text fontSize="xl">
                    Online{" "}
                    {data && (
                        <Text as="span" fontWeight="thin">
                            ({data?.currentUserCount}/{data?.maxUserCount})
                        </Text>
                    )}
                </Text>
                <Flex grow={1} />
                <Spinner
                    transition="150ms linear"
                    opacity={isFetching ? 100 : 0}
                />
            </Flex>

            <ScrollableListContent
                items={data?.users as string[]}
                error={
                    error ? "Fehler beim Laden der Spielerliste." : undefined
                }
                empty="Keine Spieler online."
                minMaxHeight={{ base: "350px", md: "265px" }}
            />

            <Spacer h={4} />
        </Card>
    );
}
