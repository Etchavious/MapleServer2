﻿using System.Collections.Generic;
using MaplePacketLib2.Tools;
using MapleServer2.Constants;
using MapleServer2.Types;

namespace MapleServer2.Packets
{
    public static class SkillBookTreePacket
    {
        public static Packet Open(Player character)
        {
            PacketWriter pWriter = PacketWriter.Of(SendOp.SKILL_BOOK_TREE);

            // Writes only skills that are learned and for the job rank tab that is opened also doesn't write default passive skills
            pWriter.WriteByte(0); // Mode (0 = open) (1 = save)
            pWriter.WriteInt(1); // Possibly always 1
            pWriter.WriteLong(character.SkillTabs[0].Id); // Skill tab id
            pWriter.WriteInt(1); // Repeat of last int and long
            pWriter.WriteLong(character.SkillTabs[0].Id); // Same as previous identifier
            pWriter.WriteUnicodeString("Build 1"); // Page name

            // Get feature skills, for now just rank 1 skills, not sure how to tell which tab is opened
            List<Skill> skills = character.SkillTabs[0].GetJobFeatureSkills(); // Get first skill tab skills only for now, uncertain of how to have multiple skill tabs
            skills.RemoveAll(x => x.Learned < 1); // Remove all unlearned skills
            skills.RemoveAll(x => x.Id == 10900051 || x.Id == 10900041); // temp remove rb default passives
            pWriter.WriteInt(skills.Count); // Skill count

            // List of learned skills for given tab in format (int skill_id) (int skill_level)
            for (int i = 0; i < skills.Count; i++)
            {
                pWriter.WriteInt(skills[i].Id);
                pWriter.WriteInt((int) skills[i].Level);
            }

            return pWriter;
        }

        public static Packet Save(Player character)
        {
            PacketWriter pWriter = PacketWriter.Of(SendOp.SKILL_BOOK_TREE);

            pWriter.WriteByte(1); // Mode (0 = open) (1 = save)
            pWriter.WriteLong(character.SkillTabs[0].Id); // Skill tab id
            pWriter.WriteLong(character.SkillTabs[0].Id); // Skill tab id
            pWriter.WriteInt(1); // Possibly always 1

            return pWriter;
        }
    }
}
